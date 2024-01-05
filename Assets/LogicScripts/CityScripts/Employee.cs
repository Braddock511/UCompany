using UnityEngine;

public class Employee : MonoBehaviour
{
    public string employeeName;
    public int age;
    public float salary;
    public string skills; // Stworzyc klase skills

    public Employee(string employeeName, int age, float salary, string skills)
    {
        this.employeeName = employeeName;
        this.age = age;
        this.salary = salary;
        this.skills = skills;
    }
}
