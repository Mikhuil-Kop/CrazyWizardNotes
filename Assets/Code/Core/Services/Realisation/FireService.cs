using System.Collections.Generic;
using System.Linq;
using CrazyWizardNotes.Interfaces;
using UnityEngine;

namespace CrazyWizardNotes.Common.Services
{
    public class FireService : SingletonBehavior<FireService>
    {
        [SerializeField]
        private int FieldWidth;
        [SerializeField]
        private int FieldHeight;
        [SerializeField]
        private int FieldCenterX;
        [SerializeField]
        private int FieldCenterY;
        [SerializeField]
        private float SquareSize;
        [SerializeField]
        private float EnergyDispersion;
        [SerializeField]
        private float EnergyDispelling;

        private float[,] FireEnergy;


        protected override void Awake()
        {
            base.Awake();

            FireEnergy = new float[FieldWidth * 2, FieldHeight * 2];
        }

        private void Update()
        {
            var subjectsService = SubjectsCollectionService.Instance;
            var subjects = subjectsService.GetAll()
                .Where(x => x.Is<IFlammable>() && x.Is<ISpacePoint>())
                .Select(x => new SpaceFire()
                {
                    Flammable = x.As<IFlammable>(),
                    SpacePoint = x.As<ISpacePoint>()
                })
                .ToArray();

            foreach (var s in subjects.Where(x => x.Flammable.Burning))
            {
                var pos = s.SpacePoint.Position;
                var x = GetX(pos);
                var y = GetY(pos);

                Debug.Log($"{x}, {y}", this);

                var energy = Mathf.Min(s.Flammable.FuelEnergyBurnoutSpeed * Time.deltaTime, s.Flammable.CurrentFuelEnergy);

                s.Flammable.CurrentFuelEnergy -= energy;
                FireEnergy[x, y] += energy;
            }

            for (int x = 0; x < FireEnergy.GetLength(0); x++)
            {
                for (int y = 0; y < FireEnergy.GetLength(1); y++)
                {
                    FireEnergy[x, y] = Mathf.Max(0, FireEnergy[x, y] - EnergyDispelling * Time.deltaTime);
                }
            }

            Debug.Log($"Суммарный нагрев: {GetEnumerable(FireEnergy).Sum()}", this);

            ShowDebug();
        }

        private int GetX(Vector2 vector)
        {
            return FieldWidth + Mathf.RoundToInt(vector.x / SquareSize) - FieldCenterX;
        }

        private int GetY(Vector2 vector)
        {
            return FieldHeight + Mathf.RoundToInt(vector.y / SquareSize) - FieldCenterY;
        }

        private Vector2 GetVec(int x, int y)
        {
            return new Vector2(
                (x + FieldCenterX - FieldWidth) * SquareSize,
                (y + FieldCenterY - FieldHeight) * SquareSize);
        }

        private IEnumerable<T> GetEnumerable<T>(T[,] array)
        {
            for (int x = 0; x < array.GetLength(0); x++)
            {
                for (int y = 0; y < array.GetLength(1); y++)
                {
                    yield return array[x, y];
                }
            }
        }

        private void ShowDebug()
        {
            var center = new Vector3(FieldCenterX, FieldCenterY, 0);

            for (int x = -FieldWidth; x <= FieldWidth; x++)
            {
                Debug.DrawLine(new Vector3(x * SquareSize, FieldHeight * SquareSize, 0) + center, new Vector3(x * SquareSize, -FieldHeight * SquareSize, 0) + center, Color.red);
            }

            for (int y = -FieldHeight; y <= FieldHeight; y++)
            {
                Debug.DrawLine(new Vector3(FieldWidth * SquareSize, y * SquareSize, 0) + center, new Vector3(-FieldWidth * SquareSize, y * SquareSize, 0) + center, Color.red);
            }
        }


        private struct SpaceFire
        {
            public IFlammable Flammable;
            public ISpacePoint SpacePoint;
        }
    }
}