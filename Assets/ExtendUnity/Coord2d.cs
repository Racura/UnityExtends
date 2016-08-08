using UnityEngine;
using System.Collections;

[System.Serializable]
public struct Coord2d {

	public bool IsEmpty { get { return x == 0 && y == 0; } }

	public int x;
	public int y;
	
	public Coord2d(int x, int y) {
		this.x = x;
		this.y = y;
	}
	
	public Coord2d Translate(int x, int y) {
		return new Coord2d(this.x + x, this.y + y);
	}
	public Coord2d Translate(Coord2d coord) {
		return new Coord2d(this.x + coord.x, this.y + coord.y);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode () 
			^ (x.GetHashCode() << 3)
			^ (y.GetHashCode() << 7);
	}

	public override bool Equals (object obj) {
		return (obj is Coord2d) && (Coord2d)obj == this;
	}
	
	public static bool operator ==(Coord2d a, Coord2d b)
	{
		return a.x == b.x && a.y == b.y;
	}

	public static bool operator !=(Coord2d a, Coord2d b)
	{
		return a.x != b.x || a.y != b.y;
	}

	public static implicit operator Vector2(Coord2d i)
	{
		return new Vector2(i.x, i.y);
	}
	
	public static Coord2d operator +(Coord2d p1, Coord2d p2) {
		return new Coord2d(p1.x + p2.x, p1.y + p2.y);
	}
	
	public static Coord2d operator -(Coord2d p1, Coord2d p2) {
		return new Coord2d(p1.x - p2.x, p1.y - p2.y);
	}
	
	public static readonly Coord2d zero = new Coord2d ();
	public static readonly Coord2d one = new Coord2d (1,1);
}