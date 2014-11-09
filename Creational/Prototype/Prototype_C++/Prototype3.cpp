// Prototype Design Pattern - Creational Category

// Discussion
// Image base class provides the mechanism for storing, finding and cloning the prototype for all derived classes. 
// Each derived class specifies a private static data member whose initialization "registers" a prototype of itself 
// with the base class. When the client asks for a "clone" of a certain type, the base class finds the prototype and 
// calls clone() on the correct derived class.

// http://sourcemaking.com/design_patterns/prototype/cpp/3

#include <iostream>

using namespace std;

enum imageType
{
	LSAT, SPOT
};

class Image
{
	public:

		virtual void draw() = 0;

		// Client calls this public static member function when it needs an instance of an Image subclass
		static Image * findAndClone(imageType type)
		{
			for (int i = 0; i < _nextSlot; i++)
			{
				if (_prototypes[i]->returnType() == type)
				{
					return _prototypes[i]->clone();
				}
			}

			return NULL;
		}

	protected:

		virtual Image * clone() = 0;
		virtual imageType returnType() = 0;
		
		// As each subclass of Image is declared, it registers its prototype with the base class
		static void addPrototype(Image * image)
		{
			_prototypes[_nextSlot++] = image;
		}

	private:

		static int _nextSlot;

		// addPrototype() saves each registered prototype here
		static Image *_prototypes[10];
		
};

int Image::_nextSlot = 0;
Image * Image::_prototypes[];

class LandSatImage : public Image
{
	public:

		imageType returnType()
		{
			return LSAT;
		}

		void draw()
		{
			cout << "LandSatImage::draw " << _id << endl;
		}

		// When clone() is called, call the one-argument constructor with a dummy argument
		Image * clone()
		{
			return new LandSatImage(1);
		}

	protected:

		// This is only called from clone()
		LandSatImage(int dummy)
		{
			_id = _count++;
		}

	private:

		// This is only called when the private static data member is initialized
		LandSatImage()
		{
			addPrototype(this);
		}

		// Nominal "state" per instance mechanism
		int _id;

		// Keeps track of the number of instances of this subclass
		static int _count;

		// Mechanism for initializing an Image subclass - this causes the default
		// constructor to be called, which registers the subclass's prototype
		static LandSatImage _landSatImage;

};

// Initialize the "state" per instance mechanism
int LandSatImage::_count = 1;

// Register the subclass's prototype (via the invocation of the private default constructor LandSatImage())
LandSatImage LandSatImage::_landSatImage;


class SpotImage: public Image
{
	public:

		imageType returnType()
		{
			return SPOT;
		}

		void draw()
		{
			cout << "SpotImage::draw " << _id << endl;
		}

		Image * clone()
		{
			return new SpotImage(1);
		}

	protected:

		SpotImage(int dummy)
		{
			_id = _count++;
		}

	private:

		// This is only called when the private static data member is initialized
		SpotImage()
		{
			addPrototype(this);
		}

		// Nominal "state" per instance mechanism
		int _id;

		// Keeps track of the number of instances of this subclass
		static int _count;

		// Mechanism for initializing an Image subclass - this causes the default
		// constructor to be called, which registers the subclass's prototype
		static SpotImage _spotImage;
};

// Initialize the "state" per instance mechanism
int SpotImage::_count = 1;

// Register the subclass's prototype (via the invocation of the private default constructor SpotImage())
SpotImage SpotImage::_spotImage;


// Simulated stream of creation requests
const int NUM_IMAGES = 8;

imageType input[NUM_IMAGES] = 
{
	LSAT, LSAT, LSAT, SPOT, LSAT, SPOT, SPOT, LSAT
};


int main()
{
	Image * images[NUM_IMAGES];

	// Given an image type, find the right prototype, and return a clone
	for (int i = 0; i < NUM_IMAGES; i++)
		images[i] = Image::findAndClone(input[i]);

	// Demonstrate that correct image objects have been cloned
	for (int i = 0; i < NUM_IMAGES; i++)
	{
		if ( images[i] != NULL )
			images[i]->draw();
	}

	// Free the dynamically allocated memory
	for (int i = 0; i < NUM_IMAGES; i++)
		delete images[i];

	system("pause");

	return 0;
}