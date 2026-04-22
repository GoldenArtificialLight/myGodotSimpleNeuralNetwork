using Godot;
using System;

using neuralNetwork.Scripts.NeuralNetwork;

namespace GodotNeuralNetwork;


public partial class Entity : CharacterBody2D
{
	/*
	  ok, lembrando da aula: Um neurônio sempre tem
	  uma ou mais entradas, uma função de agregação/soma, uma
	  função de ativação, que decide qual é a saída, e uma
	  saída. Nesse caso, a estrutura lógica seria assim:

	  Entradas:
	  - Posição X do oponente
	  - Própria posição X
	  - Posição X da bomba do oponente
	  - Posição Y da bomba do oponente (?)

	  Saídas:
	  - Move left
	  - Move right
	  - Throw bomb
	*/
	
	// brother set by parent
	public CharacterBody2D opponent;

	#nullable enable
	public CharacterBody2D? bomb;
	#nullable disable

	public NeuralNetwork nn = new([Neuron.Factory(5,3), Neuron.Factory(3,5)]);

	public const float Speed = 300.0f;

	public override void _PhysicsProcess(double delta)
	{
		MakeChoice([
			opponent.Position.X, // posição X do oponente
			Position.X,
			bomb?.Position.X == null ? -1.0f : bomb.Position.X, // posição X da bomba do oponente
		]);
		ManageMovement();
	}
	
	/*
		possible choices:
		- Move right
		- Move left
		- Stop/Stay
		- Throw bomb
	*/
	public void MakeChoice(float[] inputs)
	{
		
		if(true)
		{
			ThrowBomb();
		}
		else
		{
			// sets the movement direction to be either 1, 0 or -1
		}
	}

	// actually moves the body
	public void ManageMovement()
	{
		Vector2 velocity = Velocity;

		Vector2 direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		if (direction != Vector2.Zero)
		{
			velocity.X = direction.X * Speed;
		}
		else
		{
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
		}

		Velocity = velocity;
		MoveAndSlide();
	}

	public void ThrowBomb(){}
}
