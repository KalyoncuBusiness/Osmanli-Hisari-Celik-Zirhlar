public class Cell
{
    private Grid<Cell> grid;
    private int x, y;
    private Soldier _soldier;

    public Cell(Grid<Cell> grid, int x, int y)
    {
        this.grid = grid;
        this.x = x;
        this.y = y;
    }

    public void SetPlacedObject(Soldier transform)
    {
        this._soldier = transform;
    }

    public void ClearPlacedObject()
    {
        this._soldier = null;
    }

    public bool CanBuild => _soldier == null;

    public Soldier Soldier => _soldier;
}