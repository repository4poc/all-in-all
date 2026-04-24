function Dice() {
  const handleClick = () => {
    var randomNumber1 = Math.floor(Math.random() * 6) + 1; //1-6

    var randomDiceImage = "dice" + randomNumber1 + ".png"; //dice1.png - dice6.png

    var randomImageSource = "/assets/dices/" + randomDiceImage; //images/dice1.png - images/dice6.png

    var image1 = document.querySelectorAll("#dice1")[0];

    image1.setAttribute("src", randomImageSource);

    var randomNumber2 = Math.floor(Math.random() * 6) + 1;

    var randomDiceImage2 = "dice" + randomNumber2 + ".png";

    var randomImageSource2 = "/assets/dices/" + randomDiceImage2;

    var image2 = document.querySelectorAll("#dice2")[0];

    image2.setAttribute("src", randomImageSource2);

    //If player 1 wins
    if (randomNumber1 > randomNumber2) {
      document.querySelector("h1").innerHTML = "🚩 Play 1 Wins!";
    } else if (randomNumber2 > randomNumber1) {
      document.querySelector("h1").innerHTML = "Player 2 Wins! 🚩";
    } else {
      document.querySelector("h1").innerHTML = "Draw!";
    }
  };

  return (
    <div id="dice-container" className="container">
      <h1>Refresh Me</h1>

      <div className="dice">
        <p>Player 1</p>
        <img
          id="dice1"
          className="img1"
          src="/assets/dices/dice6.png"
          onClick={handleClick}
        />
      </div>

      <div className="dice">
        <p>Player 2</p>
        <img
          id="dice2"
          className="img2"
          src="/assets/dices/dice6.png"
          onClick={handleClick}
        />
      </div>
    </div>
  );
}

export default Dice;
