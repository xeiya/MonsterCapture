using UnityEngine;
using System.Collections.Generic;

public class CarShowroom : MonoBehaviour
{
    void Start()
    {
        Car car = new Clown();

        (car as Clown)?.EjectClown();

        int a = 5;
        int b = 5;
        int x = a == b ? 10 : 100;
        /*if (car is Clown) 
        { 
            Clown car2 = (Clown) car;
            car2.EjectClown();
        }
        */

        F1 F1Car = new F1();
        Clown clownCar = new Clown();

        F1Car.SetOwner("Bruce Mclaren");
        clownCar.SetOwner("Judah");

        List<Car> cars = new List<Car>(); 
        cars.Add(car);      //0
        cars.Add(clownCar); //1
        cars.Add(F1Car);    //2

        int i = Random.Range(0, cars.Count);
        Car raceCar1 = cars[i];
        cars.RemoveAt(i);
        i = Random.Range(0, cars.Count);
        Race(raceCar1, cars[i]);
    }

    void Race(Car car1, Car car2) 
    {
        if (car1.speed > car2.speed)
        {
            Debug.Log("HERE IS YOUR WINNER " + car1.VictorySpeech());
        }
        else 
        {
            Debug.Log("HERE IS YOUR WINNER " + car2.VictorySpeech());
        }
    }
}
