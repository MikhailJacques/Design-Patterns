// Adapter Design Pattern - Structural Category

#include <iostream>

using namespace std;

typedef int Coordinate;
typedef int Dimension;

// Desired interface
class Rectangle
{
	public:

		virtual void draw() = 0;
};

// Legacy component
class LegacyRectangle
{
	public:

		LegacyRectangle(Coordinate x1, Coordinate y1, Coordinate x2, Coordinate y2)
		{
			_x1 = x1;
			_y1 = y1;
			_x2 = x2;
			_y2 = y2;

			cout << "LegacyRectangle:  create.  (" << _x1 << "," << _y1 << ") => ("
				<< _x2 << "," << _y2 << ")" << endl;
		}

		void oldDraw()
		{
			cout << "LegacyRectangle:  oldDraw.  (" << _x1 << "," << _y1 << 
				") => (" << _x2 << "," << _y2 << ")" << endl;
		}

	private:

		Coordinate _x1;
		Coordinate _y1;
		Coordinate _x2;
		Coordinate _y2;
};

// Adapter wrapper
class RectangleAdapter: public Rectangle, private LegacyRectangle
{
	public:

		RectangleAdapter(Coordinate x, Coordinate y, Dimension w, Dimension h) : LegacyRectangle(x, y, x + w, y + h)
		{
			cout << "RectangleAdapter: create.  (" << x << "," << y << 
				"), width = " << w << ", height = " << h << endl;
		}

		virtual void draw()
		{
			cout << "RectangleAdapter: draw." << endl;
			oldDraw();
		}
};

int main()
{
	Rectangle * r = new RectangleAdapter(120, 200, 60, 40);

	r->draw();

	system("pause");

	return 0;
}