//// Prototype Design Pattern - Creational Category
//
//// A prototype pattern is used in software development when the type of objects to create 
//// is determined by a prototypical instance, which is cloned to produce new objects.
//// The Prototype design pattern allows an object to create customized objects without 
//// knowing their class or any details of how to create them.
//// This pattern is used, for example, when the inherent cost of creating a new object in the 
//// standard way (e.g., using the new keyword) is prohibitively expensive for a given application.
//
//// Intent
//// Specifying the kind of objects to create using a prototypical instance
//// Creating new objects by copying this prototype
//
//// Implementation: Declare an abstract base class that specifies a pure virtual clone() member function. 
//// Any class that needs a "polymorphic constructor" capability derives itself from the abstract base class, 
//// and implements the clone() operation.
//// The client, instead of writing code that invokes the new operator on a hard-wired class name, 
//// calls the clone() member function on the prototype, calls a factory member function with a parameter 
//// designating the particular concrete derived class desired, or invokes the clone() member function through 
//// some mechanism provided by another design pattern.
//
//#include <map>
//#include <string>
//#include <iostream>
// 
//using namespace std;
// 
//enum RECORD_TYPE
//{
//	CAR,
//	BIKE,
//	TRAIN,
//	ANIMAL,
//	PERSON
//};
// 
//// Record is the base Prototype
//class Record
//{
//	public :
// 
//		Record() {}
// 
//		virtual ~Record() {}
// 
//		virtual Record * clone() = 0;
// 
//		virtual void print() = 0;
//};
// 
//// CarRecord is a Concrete Prototype
//class CarRecord : public Record
//{
//	private:
//
//		int m_ID;
//		string m_carName;
//		
//	public:
//
//		CarRecord(string carName, int ID) : Record(), m_carName(carName), m_ID(ID) {}
// 
//		// Call the base default copy constructor
//		CarRecord(const CarRecord & carRecord) : Record(carRecord) 
//		{
//			m_carName = carRecord.m_carName;
//			m_ID = carRecord.m_ID;
//		}
// 
//		~CarRecord() {}
// 
//		Record * clone()
//		{
//			return new CarRecord(*this);
//		}
// 
//		void print()
//		{
//			cout << "Car Record" << endl
//				<< "Name  : " << m_carName << endl
//				<< "Number: " << m_ID << endl << endl;
//		}
//};
// 
// 
//// BikeRecord is the Concrete Prototype
//class BikeRecord : public Record
//{
//	private:
//
//		int m_ID;
//		string m_bikeName;
// 
//	public:
//
//		BikeRecord(string bikeName, int ID) : Record(), m_bikeName(bikeName), m_ID(ID) {}
// 
//		BikeRecord(const BikeRecord & bikeRecord) : Record(bikeRecord)
//		{
//			m_bikeName = bikeRecord.m_bikeName;
//			m_ID = bikeRecord.m_ID;
//		}
// 
//		~BikeRecord() {}
// 
//		Record * clone()
//		{
//			return new BikeRecord(*this);
//		}
// 
//		void print()
//		{
//			cout << "Bike Record" << endl 
//				<< "Name  : " << m_bikeName << endl
//				<< "Number: " << m_ID << endl << endl;
//		}
//};
// 
//// TrainRecord is the Concrete Prototype
//class TrainRecord : public Record
//{
//	private:
//
//		int m_ID;
//		string m_trainName;
// 
//	public:
//
//		TrainRecord(string trainName, int ID) : Record(), m_trainName(trainName), m_ID(ID) {}
// 
//		TrainRecord(const TrainRecord & trainRecord) : Record(trainRecord)
//		{
//			m_trainName = trainRecord.m_trainName;
//			m_ID = trainRecord.m_ID;
//		}
// 
//		~TrainRecord() {}
// 
//		Record * clone()
//		{
//			return new TrainRecord(*this);
//		}
// 
//		void print()
//		{
//			cout << "Train Record" << endl 
//				<< "Name  : " << m_trainName << endl 
//				<< "Number: " << m_ID << endl << endl;
//		}
//};
// 
//// AnimalRecord is the Concrete Prototype
//class AnimalRecord : public Record
//{
//	private:
//
//		int m_age;
//		string m_animalType;
// 
//	public:
//
//		AnimalRecord(string animalType, int age) : Record(), m_animalType(animalType), m_age(age) {}
// 
//		AnimalRecord(const AnimalRecord & animalRecord) : Record(animalRecord)
//		{
//			m_animalType = animalRecord.m_animalType;
//			m_age = animalRecord.m_age;
//		}
// 
//		~AnimalRecord() {}
// 
//		Record * clone()
//		{
//			return new AnimalRecord(*this);
//		}
// 
//		void print()
//		{
//			cout << "Animal Record" << endl 
//				<< "Type : " << m_animalType << endl 
//				<< "Age  : " << m_age << endl << endl;
//		}
//};
// 
// 
//// PersonRecord is the Concrete Prototype
//class PersonRecord : public Record
//{
//	private:
//
//		int m_age;
//		string m_personName;
// 
//	public:
//
//		PersonRecord(string personName, int age) : Record(), m_personName(personName), m_age(age) {}
// 
//		PersonRecord(const PersonRecord & personRecord) : Record(personRecord)
//		{
//			m_personName = personRecord.m_personName;
//			m_age = personRecord.m_age;
//		}
// 
//		~PersonRecord() {}
// 
//		Record * clone()
//		{
//			return new PersonRecord(*this);
//		}
// 
//		void print()
//		{
//			cout << "Person Record" << endl 
//				<< "Name : " << m_personName << endl 
//				<< "Age  : " << m_age << endl << endl;
//		}
//};
// 
// 
////  RecordFactory is the client
//class RecordFactory
//{
//	private:
//		
//		map<RECORD_TYPE, Record *> m_recordReference;
// 
//	public:
//	
//		// Populate record factory with concrete records
//		RecordFactory()
//		{
//			m_recordReference[CAR]  = new CarRecord("Ferrari", 5050);
//			m_recordReference[BIKE] = new BikeRecord("Yamaha", 2525);
//			m_recordReference[TRAIN] = new TrainRecord("Blue Train", 762);
//			m_recordReference[ANIMAL] = new AnimalRecord("Dog", 3);
//			m_recordReference[PERSON] = new PersonRecord("Bob", 25);
//		}
// 
//		~RecordFactory()
//		{
//			delete m_recordReference[CAR];
//			delete m_recordReference[BIKE];
//			delete m_recordReference[TRAIN];
//			delete m_recordReference[ANIMAL];
//			delete m_recordReference[PERSON];
//		}
// 
//		Record * createRecord(RECORD_TYPE enType)
//		{
//			return m_recordReference[enType]->clone();
//		}
//};
// 
//int main()
//{
//	RecordFactory * poRecordFactory = new RecordFactory();
// 
//	Record * poRecord;
//
//	poRecord = poRecordFactory->createRecord(CAR);
//	poRecord->print();
//	delete poRecord;
// 
//	poRecord = poRecordFactory->createRecord(BIKE);
//	poRecord->print();
//	delete poRecord;
//
//	poRecord = poRecordFactory->createRecord(TRAIN);
//	poRecord->print();
//	delete poRecord;
//
//	poRecord = poRecordFactory->createRecord(ANIMAL);
//	poRecord->print();
//	delete poRecord;
//
//	poRecord = poRecordFactory->createRecord(PERSON);
//	poRecord->print();
//	delete poRecord;
// 
//	delete poRecordFactory;
//
//	system("pause");
//
//	return 0;
//}