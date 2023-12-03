using CrazyWizardNotes.Elements.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using UNet.Core.Abstractions;
using UNet.Physics.Abstractions;
using GraphNode = LinqExtension.GraphNode<CrazyWizardNotes.Elements.Realization.ElectricService.GraphValue>;

namespace CrazyWizardNotes.Elements.Realization
{
    public class ElectricService : IElectricService
    {
        public event ElectricDischargeEventHandler OnElectricDischarge;

        public void RedistributeCharge(IEnumerable<IActor> conductors)
        {
            if (conductors == null)
            {
                throw new ArgumentNullException(nameof(conductors));
            }

            var graph = GetRedistributeGraph(conductors);

            RedistributeChargeByGraph(graph);
        }

        private void RedistributeChargeByGraph(GraphNode[] graph)
        {
            if (graph == null || graph.Length == 0) return;

            var qSum = 0f;
            var cSum = 0f;

            foreach (var node in graph)
            {
                var c = Math.Max(node.Value.Conductor.ElectricCapacity, 1e-8f);
                var q = node.Value.Conductor.ElectricCharge;
                
                qSum += q;
                cSum += c;
                
                node.Value.Supply = q;
                node.Value.OldCharge = q;
                node.Value.IncidentAbs = 0f;
                node.Value.Seen = false;
            }

            var v = qSum / cSum;

            foreach (var node in graph)
            {
                var c = Math.Max(node.Value.Conductor.ElectricCapacity, 1e-8f);
                var qf = v * c;
                node.Value.Supply -= qf;
                node.Value.NewCharge = qf;
            }

            foreach (var node in graph)
            {
                if (!node.Value.Seen)
                {
                    Walk(node, null);
                }
            }

            foreach (var node in graph)
            {
                node.Value.Through = Math.Max(0f, (node.Value.IncidentAbs - Math.Abs(node.Value.Supply)) * 0.5f);

                OnElectricDischarge?.Invoke(new ElectricDischargeEventArgs(node.Value.Actor, node.Value.OldCharge, node.Value.NewCharge, node.Value.Through));
            }

            foreach (var node in graph)
            {
                node.Value.Conductor.Discharge(node.Value.NewCharge, node.Value.Through);
            }
        }

        private static float Walk(GraphNode node, GraphNode parent)
        {
            node.Value.Seen = true;
            var flow = node.Value.Supply;

            foreach (var next in node.Neighbors)
            {
                if (next != parent && !next.Value.Seen)
                {
                    flow += Walk(next, node);
                }
            }

            if (parent != null)
            {
                var a = Math.Abs(flow);
                node.Value.IncidentAbs += a;
                parent.Value.IncidentAbs += a;
            }

            return flow;
        }

        private static GraphNode[] GetRedistributeGraph(IEnumerable<IActor> roots)
        {
            var graph = ActorsAsGraphNode(roots)
                .ToGraph(x => ActorsAsGraphNode(x.Touches.TouchingActors), GraphValueEqualityComparer.Instance)
                .ToArray();

            return graph;
        }

        private static IEnumerable<GraphValue> ActorsAsGraphNode(IEnumerable<IActor> actors)
        {
            return actors
                .WithComponents<IElectricConductor, ITouches>()
                .Where(x => x.AllComponentsPresent)
                .Select(x => new GraphValue
                {
                    Actor = x.Actor,
                    Conductor = x.Component1,
                    Touches = x.Component2
                });
        }

        #region Внутренние классы

        internal sealed class GraphValue
        {
            public IActor Actor { get; set; }
            public ITouches Touches { get; set; }
            public IElectricConductor Conductor { get; set; }

            public bool Seen { get; set; }
            public float Supply { get; set; }
            public float IncidentAbs { get; set; }
            public float Through { get; set; }
            public float OldCharge { get; set; }
            public float NewCharge { get; set; }
        }

        internal sealed class GraphValueEqualityComparer : IEqualityComparer<GraphValue>
        {
            public static GraphValueEqualityComparer Instance = new();

            public bool Equals(GraphValue x, GraphValue y)
            {
                return x.Actor == y.Actor;
            }

            public int GetHashCode(GraphValue obj)
            {
                return obj.Actor.GetHashCode();
            }
        }

        #endregion
    }
}
