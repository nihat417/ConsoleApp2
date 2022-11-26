namespace Iterator;

struct Employee
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
}

interface IAggregate
{
    IIterator CreateIterator();
}

interface IIterator
{
    bool HasItem();
    Employee NextItem();
    Employee CurrentItem();
}

class EmployeeAggregate : IAggregate
{
    List<Employee> Employees = new List<Employee>();
    public void Add(Employee Model) => Employees.Add(Model);
    public Employee GetItem(int index) => Employees[index];
    public int Count { get => Employees.Count; }
    public IIterator CreateIterator() => new EmployeeIterator(this);
}


class EmployeeIterator : IIterator
{
    EmployeeAggregate aggregate;
    int currentindex;
    public EmployeeIterator(EmployeeAggregate aggregate) => this.aggregate = aggregate;
    public Employee CurrentItem() => aggregate.GetItem(currentindex);
    public bool HasItem()
    {
        if (currentindex < aggregate.Count)
            return true;
        return false;
    }
    public Employee NextItem()
    {
        if (HasItem())
            return aggregate.GetItem(currentindex++);
        return new Employee();
    }
}

class Program
{

    static void Main()
    {
        EmployeeAggregate aggregate = new EmployeeAggregate();
        aggregate.Add(new Employee { Id = 1, Name = "niko", Surname = "akremi" });
        aggregate.Add(new Employee { Id = 2, Name = "samir", Surname = "purrengi" });


        IIterator iterator = aggregate.CreateIterator();
        while (iterator.HasItem())
        {
            Console.WriteLine($"ID : {iterator.CurrentItem().Id}\nName : {iterator.CurrentItem().Name}\nSurname : {iterator.CurrentItem().Surname}\n*****");
            iterator.NextItem();
        }


    }
}//