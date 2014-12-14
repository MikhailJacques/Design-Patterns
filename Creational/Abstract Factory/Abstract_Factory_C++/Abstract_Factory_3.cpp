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
	IDumb * dumb_phone = NULL;
	ISmart * smart_phone = NULL;
	PhoneFactory * factory = NULL;

	// Note that the use of smart pointers will get rid of the delete statements below.
	// The issue that smart pointers do not permit instantiation of abstract classes.
	// unique_ptr<PhoneFactory> factory = NULL;
	// shared_ptr<PhoneFactory> factory = NULL;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::SAMSUNG);
	dumb_phone = factory->GetDumb();
	smart_phone = factory->GetSmart();
	cout << "Dumb phone from Samsung: " << dumb_phone->Name() << "\n"; 
	cout << "Smart phone from Samsung: " << smart_phone->Name() << "\n\n";
	delete dumb_phone;
	delete smart_phone;
	delete factory;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::HTC);
	dumb_phone = factory->GetDumb();
	smart_phone = factory->GetSmart();
	cout << "Dumb phone from HTC: " << dumb_phone->Name() << "\n";
	cout << "Smart phone from HTC: " << smart_phone->Name() << "\n\n";
	delete dumb_phone;
	delete smart_phone;
	delete factory;
	
	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::NOKIA);
	dumb_phone = factory->GetDumb();
	smart_phone = factory->GetSmart();
	cout << "Dumb phone from Nokia: " << dumb_phone->Name() << "\n";
	cout << "Smart phone from Nokia: " << smart_phone->Name() << "\n\n";	
	delete dumb_phone;
	delete smart_phone;
	delete factory;

	factory = PhoneFactory::CreateFactory(PhoneFactory::VENDORS::APPLE);
	dumb_phone = factory->GetDumb();
	smart_phone = factory->GetSmart();
	cout << "Dumb phone from Apple: " << dumb_phone->Name() << "\n";
	cout << "Smart phone from Apple: " << smart_phone->Name() << "\n\n";	
	delete dumb_phone;
	delete smart_phone;
	delete factory;

	cin.get(); // 	getchar();

	return 0;
}
