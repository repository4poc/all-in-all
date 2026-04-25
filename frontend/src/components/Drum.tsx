function Drum() {
  const keys = ["crash", "kick", "snare", "tom1", "tom2", "tom3", "tom4"];

  const handleClick = (key: string) => {
    const audio = new Audio(`/assets/drum/sounds/${key}.mp3`);
    audio.play();
  };

  return (
    <>
      <div className="drum-container">
        <h1>Drum 🥁 Kit</h1>
        <div>
          {keys.map((key) => (
            <button
              key={key}
              className={`${key} drum`}
              onClick={() => handleClick(key)}
            >
              <img
                src={`/assets/drum/images/${key}.png`}
                alt={key}
                className="drum-img"
              />
            </button>
          ))}
        </div>
      </div>
    </>
  );
}

export default Drum;
