// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

Battleship b = new Battleship();
String result = b.solution(3, "1A 1B,2C 2C", "1B");
Console.WriteLine(result);

/// <summary>
/// Battleship (game)
/// </summary>
public class Battleship
{
	/// <summary>
	/// constructor
	/// </summary>
	public Battleship()
	{
	}

	public String solution(int N, String S, String T)
	{
		List<Ship> ships = parseShips(S, N * N);
		List<Point> hits = parseHits(T, N * N);

		int touched = 0, sunken = 0;

		foreach (Ship ship in ships)
		{
			int touching = ship.getHits(hits);
			if (touching > 0)
			{
				if (touching == ship.getSize())
				{
					sunken++; // Ship sunken
				}		
				else
				{
					touched++; // Ship toched
				}
			}
		}

		return "" + sunken + "," + touched;
	}

	/// <summary>
	/// set hits
	/// </summary>
	/// <param name="hits"></param>
	/// <param name="maxHits"></param>
	/// <returns></returns>
	public List<Point> parseHits(String hits, int maxHits)
	{
		List<Point> hitsList = new List<Point>(maxHits);
		String[] coords = hits.Split(" ");

		foreach (String coord in coords)
		{
			hitsList.Add(new Point(coord));
		}
		return hitsList;
	}

	/// <summary>
	/// arrange ships
	/// </summary>
	/// <param name="ships"></param>
	/// <param name="maxShips"></param>
	/// <returns></returns>
	public List<Ship> parseShips(String ships, int maxShips)
	{
		List<Ship> shipsList = new List<Ship>(maxShips);
		String[] shipsCoords = ships.Split(",");
		foreach (String shipCoord in shipsCoords)
		{
			String[] coords = shipCoord.Split(" ");
			shipsList.Add(new Ship(new Point(coords[0]), new Point(coords[1])));
		}
		return shipsList;
	}
}

public class Point
{
	int x;
	int y;

	public Point(String coord)
	{
		x = (coord.ToUpper()[1]) - ('A');
		y = (coord[0]) - ('1');
	}

	public int getX()
	{
		return x;
	}

	public int getY()
	{
		return y;
	}

	public Boolean greaterOrEqual(Point other)
	{
		return x >= other.x && y >= other.y;
	}

    public String toString()
    {
        return "(" + x + ", " + y + ")";
    }
}

public class Ship
{
	private Point topLeft;
	private Point bottomRight;

	public Ship(Point topLeft, Point bottomRight)
	{
		this.topLeft = topLeft;
		this.bottomRight = bottomRight;
	}

    public Point getTopLeft()
    {
        return topLeft;
    }

    public Point getBottomRight()
    {
        return bottomRight;
    }

    public int getSize()
	{
		return (Math.Abs(topLeft.getX() - bottomRight.getX()) + 1)
				* (Math.Abs(topLeft.getY() - bottomRight.getY()) + 1);
	}

    public String toString()
    {
        return "(" + topLeft + ", " + bottomRight + ")";
    }

    public int getHits(List<Point> shots)
	{
		int hits = 0;

		foreach (Point shot in shots)
		{
			if (shot.greaterOrEqual(topLeft) && bottomRight.greaterOrEqual(shot))
			{
				hits++;
			}
		}

		return hits;
	}
}
