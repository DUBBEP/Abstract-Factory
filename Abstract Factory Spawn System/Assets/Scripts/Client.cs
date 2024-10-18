using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Client : MonoBehaviour
{
    private int NumberOfWheels;
    private bool Engine;
    private int Passengers;
    private bool Cargo;

    public TextMeshProUGUI notificationText;
    public TextMeshProUGUI OutputText;

    private int numOfMessages = 0;


    public void OnCreateVehicle()
    {
        if (numOfMessages > 0)
            return;

        IVehicle v = GetVehicle(MakeNewRequirements());
        OutputText.text = v.ToString();
    }

    public void UpdateEngine(Toggle toggle) { Engine = toggle.isOn; }
    public void UpdateCargo(Toggle toggle) { Cargo = toggle.isOn; }
    public void UpdateWheels(TMP_InputField inputField)
    {
        if (int.TryParse(inputField.text, out NumberOfWheels) == false)
            StartCoroutine(ThrowMessage("Invalid Weel Count"));
    }
    public void UpdatePassengeres(TMP_InputField inputField)
    {
        if (int.TryParse(inputField.text, out Passengers) == false)
            StartCoroutine(ThrowMessage("Invalid PassengerCount"));
    }

    IEnumerator ThrowMessage(string message)
    {
        Debug.Log("Throwing Message");
        numOfMessages++;
        int i = 0;
        while (i < numOfMessages)
        {
            {
                notificationText.text = notificationText.text + "\n" + message;
                yield return new WaitForSeconds(3);

                if (i == numOfMessages)
                    ++i;
                else
                {
                    notificationText.text = "";
                    numOfMessages = 0;
                }
            }
        }
    }

    static IVehicle GetVehicle(VehicleRequirements requirements)
    {
        // based on requirements.Engine
        // choose a motorvehicle factory or a cycle factory
        // call create on the factory to get an appropriate vehicle
        // and return it

        //VehicleFactory factory = new VehicleFactory();

        //if (requirements.Engine)
        //{
        //    return factory.MotorVehicleFactory().Create(requirements);
        //}

        //return factory.CycleFactory().Create(requirements);

        VehicleFactory factory = new VehicleFactory(requirements);
        return factory.Create();
    }

    VehicleRequirements MakeNewRequirements()
    {
        VehicleRequirements requirements = new VehicleRequirements();
        requirements.NumberOfWheels = Mathf.Max(NumberOfWheels, 0);
        requirements.Engine = Engine;
        requirements.Cargo = Cargo;
        requirements.Passengers = Mathf.Max(Passengers, 0);
        return requirements;
    }
}