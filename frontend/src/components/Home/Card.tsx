function Card() {
  return (
    <div id="flex-container">
      <div
        className="card"
        style={{
          position: "relative",
          top: "100px",
          width: "18rem",
          backgroundColor: "lightgrey",
        }}
      >
        <img src="/assets/img1.jpg" className="card-img-top" alt="..." />

        <div className="card-body">
          <h5 className="card-title">Card title</h5>

          <p className="card-text">
            Some quick example text to build on the card title and make up the
            bulk of the card’s content.
          </p>
          <div id="flex-container-button">
            <a href="/assets/img1.jpg" className="btn btn-success">
              Submit
            </a>
            <button type="button" className="btn btn-danger">
              Cancel
            </button>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Card;
