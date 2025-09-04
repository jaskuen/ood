using Ducks;
using Ducks.DDuck.Behaviour.Fly;
using Ducks.DDuck.Implementation;

var mallardDuck = new MallardDuck();
DuckFunctions.PlayWithDuck(mallardDuck);

var redheadDuck = new RedheadDuck();
DuckFunctions.PlayWithDuck(redheadDuck);

var rubberDuck = new RubberDuck();
DuckFunctions.PlayWithDuck(rubberDuck);

var decoyDuck = new DecoyDuck();
DuckFunctions.PlayWithDuck(decoyDuck);

var modelDuck = new ModelDuck();
DuckFunctions.PlayWithDuck(modelDuck);

modelDuck.SetFlyBehaviour(FlyBehaviourFactory.FlyWithWings());
DuckFunctions.PlayWithDuck(modelDuck);