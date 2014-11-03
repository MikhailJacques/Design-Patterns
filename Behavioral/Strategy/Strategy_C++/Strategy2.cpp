// Strategy Design Pattern - Behavioral Category

// The Strategy pattern suggests: encapsulating an algorithm in a class hierarchy, 
// having clients of that algorithm hold a pointer to the base class of that hierarchy, 
// and delegating all requests for the algorithm to that "anonymous" contained object.

// In this example, the Justify base class knows how to collect a paragraph of input 
// and implement the skeleton of the "format" algorithm. It defers some details of each 
// individual algorithm to the "justify" member which is supplied by each concrete 
// derived class of Justify. The TextJustification class models an application class that would 
// like to leverage the services of a run-time-specified derived "Justify" object.

// http://sourcemaking.com/design_patterns/strategy/cpp/1

// Note: Make sure to have a quote.txt file in the project directory
// Contents of the quote.txt file used to test this program:
// The quick brown fox jumps over the lazy dog.

#include <string>
#include <fstream>
#include <iostream>

#define BUF_SIZE 80
#define WORD_SIZE 30

using namespace std;

class Justify;

class TextJustification
{
	public:

		enum JustificationType
		{
			Dummy, Left, Right, Center
		};

		TextJustification()
		{
			_strategy = NULL;
		}

		void setJustification(int type, int width);

		void applyJustification();

	private:
		
		Justify *_strategy;
};

class Justify
{
	public:

	Justify(unsigned int width) : _width(width) { }

	void format()
	{
		char line[BUF_SIZE], word[WORD_SIZE];

		ifstream inFile("quote.txt", ios::in);

		line[0] = '\0';

		// Read one word at a time
		inFile >> word;

		strcat_s(line, BUF_SIZE, word);

		while (inFile >> word)
		{
			if (strlen(line) + strlen(word) + 1 > _width)
				justify(line);	// Virtual function
			else
				strcat_s(line, BUF_SIZE, " ");

			strcat_s(line, BUF_SIZE, word);
		}

		justify(line);			// Virtual function
	}

	protected:

		unsigned int _width;

	private:

		virtual void justify(char * line) = 0;
};

class LeftJustify : public Justify
{
	public:

		LeftJustify(int width) : Justify(width) { }

	private:
		
		void justify(char * line)
		{
			cout << line << endl;

			line[0] = '\0';
		}
};

class RightJustify : public Justify
{
	public:

		RightJustify(int width) : Justify(width) { }

	private:

		void justify(char * line)
		{
			char buf[BUF_SIZE];

			int offset = _width - strlen(line);

			if ( offset < 0 || offset > BUF_SIZE )
				offset = 0;

			// Fill in block of memory with whitespace characters
			// void * memset ( void * ptr, int value, size_t num );
			// Sets the first numb of bytes of the block of memory pointed by ptr to the specified 
			// value, which although passed as an integer, is interpreted as an unsigned char.
			memset(buf, ' ', BUF_SIZE);

			// strcpy(&(buf[offset]), line);
			// Note that strcpy_s is non-standard and MS specific
			strcpy_s(&(buf[offset]), BUF_SIZE - offset, line);

			cout << buf << endl;

			line[0] = '\0';
		}
};

class CenterJustify : public Justify
{
	public:

		CenterJustify(int width) : Justify(width) { }

	private:
	
		void justify(char * line)
		{
			char buf[BUF_SIZE];
			int offset = (_width - strlen(line)) / 2;

			if ( offset < 0 || offset > BUF_SIZE )
				offset = 0;

			// Fill in block of memory with whitespace characters
			memset(buf, ' ', BUF_SIZE);

			// strcpy(&(buf[offset]), line);
			// Note that strcpy_s is non-standard and MS specific
			strcpy_s(&(buf[offset]), BUF_SIZE - offset, line);

			cout << buf << endl;

			line[0] = '\0';
		}
};

void TextJustification::setJustification(int type, int width)
{
	delete _strategy;

	if (type == Left)
		_strategy = new LeftJustify(width);
	else if (type == Right)
		_strategy = new RightJustify(width);
	else if (type == Center)
		_strategy = new CenterJustify(width);
	else
		_strategy = new LeftJustify(width); // default
}

void TextJustification::applyJustification()
{
	_strategy->format();
}

int main()
{
	TextJustification test;
	int answer, width;

	cout << "Exit(0) Left(1) Right(2) Center(3): ";
	cin >> answer;

	while (answer)
	{
		cout << "Width: ";
		cin >> width;

		// Set the justification strategy
		test.setJustification(answer, width);

		// Justify text according to the preset justification strategy
		test.applyJustification();

		cout << "Exit(0) Left(1) Right(2) Center(3): ";
		cin >> answer;
	}

	cin.get();

	return 0;
}