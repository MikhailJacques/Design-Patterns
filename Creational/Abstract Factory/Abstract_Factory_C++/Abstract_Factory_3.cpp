// Abstract Factory - Creational Patterns Category

#include <memory>
#include <string>
#include <iostream>
 
using namespace std;

class ISmart
{
	public:
		virtual string Name() = 0;

};

class GalaxyS5 : public ISmart
{
	public:
		string Name()
		{
			return "GalaxyS5";
		}
};

class Titan : public ISmart
{
	public:
		string Name()
		{
			return "Titan";
		}
};

class Lumia : public ISmart
{
	public:
		string Name()
		{
			return "Lumia";
		}
};

class iPhone6 : public ISmart
{
	public:
		string Name()
		{
			return "iPhone6";
		}
};

class IDumb
{
	public:
		virtual string Name() = 0;
};

class Asha : public IDumb
{
	public:
		string Name()
		{
			return "Asha";
		}
};

class Primo : public IDumb
{
	public:
		string Name()
		{
			return "Guru";
		}
};

class Genie : public IDumb
{
	public:
		string Name()
		{
			return "Genie";
		}
};

class iPhone : public IDumb
{
	public:
		string Name()
		{
			return "iPhone";
		}
};

// Abstract factory
class PhoneFactory
{	
	public:
		enum VENDORS
		{
			SAMSUNG,
			HTC,
			NOKIA,
			APPLE
		};

		virtual ISmart * GetSmart() = 0;
		virtual IDumb * GetDumb() = 0;

		static PhoneFactory * CreateFactory(VENDORS factory);	
};

// Concrete factory
class SamsungFactory : public PhoneFactory
{
	public:
		ISmart * GetSmart()
		{
			return new GalaxyS5();
		}

		IDumb * GetDumb()
		{
			return new Primo();
		}
};

// Concrete factory
class HTCFactory : public PhoneFactory
{
	public:
		ISmart * GetSmart()
		{
			return new Titan();
		}

		IDumb * GetDumb()
		{
			return new Genie();
		}
};

// Concrete factory
class NokiaFactory : public PhoneFactory
{
	public: 
		ISmart * GetSmart()
		{
			return new Lumia();
		}

		IDumb * GetDumb()
		{
			return new Asha();
		}
};

// Concrete factory
class AppleFactory : public PhoneFactory
{
	public: 
		ISmart * GetSmart()
		{
			return new iPhone6();
		}

		IDumb * GetDumb()
		{
			return new iPhone();
		}
};

PhoneFactory * PhoneFactory::CreateFactory(VENDORS factory)
{
	if(factory == VENDORS::SAMSUNG)
	{
		return new SamsungFactory();
	}
	else if(factory == VENDORS::HTC)
	{
		return new HTCFactory();
	}
	else if(factory == VENDORS::NOKIA)
	{
		return new NokiaFactory();
	}
	else if(factory == VENDORS::APPLE)
	{
		return new AppleFactory();
	}
}

int main(int argc, char* argv[])
{
	PhoneFactory * factory = NULL;
	// Note that the use of smart pointers will get rid of the delete statements below
	// The issue that smart pointers do not permit instantiation of abstract classes.
	// unique_ptr<PhoneFactory> factory = NULL;
	// shared_ptr<PhoneFactory> factory = NULL;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::SAMSUNG);
	cout << "Dumb phone from Samsung: " << factory->GetDumb()->Name() << "\n"; 
	cout << "Smart phone from Samsung: " << factory->GetSmart()->Name() << "\n\n";
	delete factory->GetDumb();
	delete factory->GetSmart();
	delete factory;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::HTC);
	cout << "Dumb phone from HTC: " << factory->GetDumb()->Name() << "\n";
	cout << "Smart phone from HTC: " << factory->GetSmart()->Name() << "\n\n";
	delete factory->GetDumb();
	delete factory->GetSmart();
	delete factory;
	
	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::NOKIA);
	cout << "Dumb phone from Nokia: " << factory->GetDumb()->Name() << "\n";
	cout << "Smart phone from Nokia: " << factory->GetSmart()->Name() << "\n\n";	
	delete factory->GetDumb();
	delete factory->GetSmart();
	delete factory;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::APPLE);
	cout << "Dumb phone from Apple: " << factory->GetDumb()->Name() << "\n";
	cout << "Smart phone from Apple: " << factory->GetSmart()->Name() << "\n\n";	
	delete factory->GetDumb();
	delete factory->GetSmart();
	delete factory;

	cin.get(); // 	getchar();

	return 0;
}
