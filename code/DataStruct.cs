using UnityEngine;
using Units;

[System.Serializable]
public class DataStruct : System.Object
{
	public int id;
	public Unit1 Unit1;
	public Unit2 Unit2;
	public Unit3 Unit3;
	public Unit4 Unit4;
	public Unit5 Unit5;
	public Unit6 Unit6;

	public override string ToString()
	{
		return "id: " + id + " || " + Unit1 + " || " + Unit2 + " || " + Unit3 + " || " + Unit4 + " || " + Unit5 + " || " + Unit6;
	}
}