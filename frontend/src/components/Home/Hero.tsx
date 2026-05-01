function Hero() {
  return (
    <div className="px-4 pt-1 my-5 text-center border-bottom text-white">
      <h1 className="display-4 fw-bold">Where Peace Meets the Earth</h1>

      <div className="col-lg-8 mx-auto">
        <p className="lead mb-4">
          In every walk with nature, one receives far more than he seeks more
          and more...
        </p>
      </div>

      <div className="overflow-hidden" style={{ maxHeight: "70vh" }}>
        <div className="container px-5">
          <img
            src="/assets/shiraito-waterfall-autumn-japan.jpg"
            className="img-fluid border rounded-3 shadow-lg mb-4"
            alt="Example"
            width="700px"
            height="700px"
            loading="lazy"
          />
        </div>
      </div>
    </div>
  );
}

export default Hero;
