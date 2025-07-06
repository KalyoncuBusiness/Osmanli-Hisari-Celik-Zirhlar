public class CellPosition
{
    private int _cellX, _cellY;

    public int CellX => _cellX;
    public int CellY => _cellY;

    public CellPosition(int cellX, int cellY)
    {
        this._cellX = cellX;
        this._cellY = cellY;
    }
    public override bool Equals(object obj)
    {
        if (obj == null) return false;
        if (!(obj is CellPosition)) return false;

        return ToString() == ((CellPosition)obj).ToString();
    }

    public override int GetHashCode()
    {
        return _cellX.GetHashCode() ^ _cellY.GetHashCode();
    }

    public override string ToString()
    {
        return _cellX + ", " + _cellY;
    }
}
