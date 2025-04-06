using NicksPizza.Models;

namespace NicksPizza.Services;

public static class PizzaService
{
    static LinkedList<Pizza> Pizzas { get; } = new();
    static int nextId = 3;

    static PizzaService()
    {
        Pizzas.AddLast(new Pizza { Id = 1, Name = "Italian", IsGlutenFree = false });
        Pizzas.AddLast(new Pizza { Id = 2, Name = "Veggie", IsGlutenFree = true });
    }

    public static LinkedList<Pizza> GetAll() => Pizzas;

    public static Pizza? Get(int id) => Pizzas.FirstOrDefault(p => p.Id == id);

    public static void Add(Pizza pizza)
    {
        pizza.Id = nextId++;
        Pizzas.AddLast(pizza);
    }

    public static void Delete(int id)
    {
        var pizza = Get(id);
        if (pizza is null)
            return;

        Pizzas.Remove(pizza);
    }

    public static void Update(Pizza pizza)
    {
        var pizzaNode = Pizzas.FirstOrDefault(item => item.Id == pizza.Id);
        if (pizzaNode is null)
            return;

        pizzaNode.Name = pizza.Name;
        pizzaNode.IsGlutenFree = pizza.IsGlutenFree;
    }
}
