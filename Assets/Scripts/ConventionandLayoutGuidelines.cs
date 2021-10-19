using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConventionandLayoutGuidelines : MonoBehaviour
{
    //---------------------Convention name---------------------------------
    // tên biến theo camelCase :
    // vd : var userName , int numberOfCharacter

    // tên method khi có return thì phải dùng từ get đứng trước 
    // vd : method return về 1 số nào đó => public int GetNumberOfCharacter(){}

    // tên Event sẽ luôn bắt đầu từ On 
    // vd: OnChange , OnAction v..v..

    // readonly theo Camel with underscore sẽ bắt đầu bằng _
    // vd: readonly  _nameOfPlayer;

    //  constant theo All Caps 
    // vd: const TIMER , const INFO , const DOOM 

    // Static Readonly Field THEO Pascal 
    // vd : static readonly int Age;


    // --------------- Luôn theo thứ tự public -> protected -> private-----------

    //Field => nằm ở cấp class
    // Reference 
    [SerializeField]
    private GameObject rightSpawn;
    [SerializeField]
    private GameObject leftSpawn;
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private GameObject iConSteelItem;

    //------------------public scope--------------------
    public const string STATS = "ONLY";
    public static string HEALTH = "HEALTH";
    public static readonly string NameOfCharacter = "My Name";
    public readonly float _neverChange = 120;

    // List sẽ để cùng với List 
    public List<int> number = new List<int>();
    public List<int> numberOfMonster = new List<int>();

    // Array sẽ để cùng với Array 
    public GameObject[] listOfMonster;
    public GameObject[] listOfHuman;


    public int y;
    public int a;

    //------------------protected scope---------------------
    protected string name;
    protected GameObject b;
   
    //------------------private scope-------------------------
    private string password;
    private string info;

    //Event 
    //------------------public scope--------------------
    public static event Action<int, int, int> EventOnStatus;

    //------------------protected scope---------------------
    protected static event Action<int, int, int> EventOnState;

    //------------------private scope-------------------------
    protected static event Action<int, int, int> EventOnHealth;



    //Properties 
    public string Name { get => name; set => name = value; }
    
    //Constructor
    public  ConventionandLayoutGuidelines()
    {
        // hàm khởi tạo 
    }


    //---------------------------METHOD ---------------------------------------

    //------------- Start Scope Unity Method --------------------------
    // Start is called before the first frame update
    void Start()
    {
        //Variable => nằm trong method 
      //  int b = 5;
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    //Collider 
    private void OnCollisionEnter(Collision collision)
    {
        
    }
    private void OnCollisionExit(Collision collision)
    {
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
    }
    //-------------End Scope Unity Method --------------------------

    //My Method
    public virtual string GetNameFromUser()
    {
        return "";
    }
    public void SayYourName()
    {

    }
    
    public void MoveForWard()
    {

    }

    protected int GetValueAfterCombine()
    {
        return 0;
    }

    private float GetNumberFloat()
    {
        var saveValue = GetTong(5,6);
        return 0f;
    }

    private int GetTong(int a , int b)
    {
        return a + b;
    }
}
