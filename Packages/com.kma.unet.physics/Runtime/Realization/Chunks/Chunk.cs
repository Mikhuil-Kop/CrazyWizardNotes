namespace UNet.Physics.Realization
{
    public class Chunk
    {
        private float CellSize { get; set; } = 0.5f;
        private int CellsPerChunk { get; set; } = 30;
        private float ChunkSize => ChunkSize * CellsPerChunk;
    }
}
