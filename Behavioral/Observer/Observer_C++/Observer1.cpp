// Observer Design Pattern - Behavioral Category

// The Observer Pattern defines a one-to-many dependency between objects so that when 
// one object changes state, all its dependents are notified and updated automatically.

// Problem 
// In one place or many places in the application we need to be aware about a system event 
// or an application state change. We'd like to have a standard way of subscribing to 
// listening for system events and a standard way of notifying the interested parties. 
// The notification should be automated after an interested party subscribed to the system 
// event or application state change. There also should be a way to unsubscribe.

// Forces 
// Observables (subjects) and observers (dependents) should be represented by objects. 
// The observable objects (subjects) notify the observer objects (dependents).

// Solution 
// After subscribing the listening objects are notified by a way of method call.

// http://en.wikibooks.org/wiki/C%2B%2B_Programming/Code/Design_Patterns/Behavioral_Patterns#Observer
// http://sourcemaking.com/design_patterns/observer
// http://www.oodesign.com/observer-pattern.html
// http://www.codeproject.com/Articles/769084/Observer-Pattern-Csharp

#include <list>
#include <iostream>
#include <algorithm>

using namespace std;
 
// The Abstract Observer
class ObserverBoardInterface
{
	public:
		
		virtual void update(double a, double b, double c) = 0;
};
 
// Abstract Interface for Displays
class DisplayBoardInterface
{
	public:
	
		virtual void show() = 0;
};
 
// The Abstract Subject
class WeatherDataInterface
{
	public:

		virtual void registerObserver(ObserverBoardInterface * ob) = 0;
		virtual void removeObserver(ObserverBoardInterface * ob) = 0;
		virtual void notifyObserver() = 0;
};
 
// The Concrete Subject
class WeatherData: public WeatherDataInterface
{
	public:

		void SensorDataChange(double a, double b, double c)
		{
			_humidity = a;
			_temperature = b;
			_pressure = c;

			notifyObserver();
		}
 
		void registerObserver(ObserverBoardInterface * ob)
		{
			_observers.push_back(ob);
		}
 
		void removeObserver(ObserverBoardInterface * ob)
		{
			_observers.remove(ob);
		}

	protected:

		void notifyObserver()
		{
			list<ObserverBoardInterface *>::iterator pos = _observers.begin();

			while (pos != _observers.end())
			{
				((ObserverBoardInterface *)(*pos))->update(_humidity, _temperature, _pressure);
				(dynamic_cast<DisplayBoardInterface *>(*pos))->show();
				++pos;
			}
		}
 
	private:

		double        _humidity;
		double        _temperature;
		double        _pressure;
		list<ObserverBoardInterface *> _observers;
};


// A Concrete Observer
class CurrentConditionBoard : public ObserverBoardInterface, public DisplayBoardInterface
{
	public:

		CurrentConditionBoard(WeatherData & data) : _weather_data(data)
		{
			_weather_data.registerObserver(this);
		}

		void show()
		{
			cout << "_____CurrentConditionBoard_____" << endl;
			cout << "Humidity:    " << _humidity << endl;
			cout << "Temperature: " << _temperature << endl;
			cout << "Pressure:    " << _pressure << endl;
			cout << "_______________________________\n" << endl;
		}
 
		void update(double h, double t, double p)
		{
			_humidity = h;
			_temperature = t;
			_pressure = p;
		}
 
	private:

		double _humidity;
		double _temperature;
		double _pressure;
		WeatherData & _weather_data;
};
 
// A Concrete Observer
class StatisticBoard : public ObserverBoardInterface, public DisplayBoardInterface
{
	public:

		StatisticBoard(WeatherData & data) : _max_temperature(-1000), 
			_min_temperature(1000), _avg_temperature(0), _count(0), _weather_data(data)
		{
			_weather_data.registerObserver(this);
		}
 
		void show()
		{
			cout << "________StatisticBoard_________" << endl;
			cout << "Lowest  temperature: " << _min_temperature << endl;
			cout << "Highest temperature: " << _max_temperature << endl;
			cout << "Average temperature: " << _avg_temperature << endl;
			cout << "_______________________________\n" << endl;
		}
 
		void update(double h, double temperature, double p)
		{
			++_count;

			if (temperature > _max_temperature)
			{
				_max_temperature = temperature;
			}

			if (temperature < _min_temperature)
			{
				_min_temperature = temperature;
			}

			_avg_temperature = (_avg_temperature * (_count - 1) + temperature) / _count;
		}
 
	private:

		int _count;
		double _max_temperature;
		double  _min_temperature;
		double _avg_temperature;
		WeatherData & _weather_data;
};
 
 
int main(int argc, char *argv[])
{
	WeatherData * weather_data = new WeatherData;
	CurrentConditionBoard * current_board = new CurrentConditionBoard(* weather_data);
	StatisticBoard * statistic_board = new StatisticBoard(* weather_data);
 
	weather_data->SensorDataChange(10.2, 28.2, 1001);
	weather_data->SensorDataChange(12, 30.12, 1003);
	weather_data->SensorDataChange(10.2, 26, 806);
	weather_data->SensorDataChange(10.3, 35.9, 900);
 
	weather_data->removeObserver(current_board);
 
	weather_data->SensorDataChange(100, 40, 1900);  
 
	delete weather_data;
	delete current_board;
	delete statistic_board;

	cin.get();
 
	return 0;
}