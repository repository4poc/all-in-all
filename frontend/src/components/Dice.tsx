import { useState } from "react";

function Dice() {
  const [result, setResult] = useState("");

  const handleClick = () => {
    var randomNumber1 = Math.floor(Math.random() * 6) + 1; //1-6
    const randomDiceImage = "dice" + randomNumber1 + ".png"; //dice1.png - dice6.png
    const randomImageSource = "/assets/dices/" + randomDiceImage; //images/dice1.png - images/dice6.png
    const image1 = document.querySelectorAll("#dice1")[0];
    image1.setAttribute("src", randomImageSource);

    const randomNumber2 = Math.floor(Math.random() * 6) + 1;
    const randomDiceImage2 = "dice" + randomNumber2 + ".png";
    const randomImageSource2 = "/assets/dices/" + randomDiceImage2;
    const image2 = document.querySelectorAll("#dice2")[0];
    image2.setAttribute("src", randomImageSource2);

    //If player 1 wins
    if (randomNumber1 > randomNumber2) {
      setResult("🚩 Player 1 Wins!");
    } else if (randomNumber2 > randomNumber1) {
      setResult("Player 2 Wins! 🚩");
    } else {
      setResult("Draw!");
    }
  };

  return (
    <div id="dice-container" className="container">
      <h1>Refresh Me</h1>
      <div id="result">{result}</div>

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
