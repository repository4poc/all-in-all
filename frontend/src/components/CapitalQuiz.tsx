function CapitalQuiz() {
  return (
    <main className="form-signin w-100 m-auto">
      <form action="/api/submit" method="POST">
        <h1 className="h3 mb-5 mt-5 fw-normal">Capital Quiz</h1>

        <div className="form-floating mb-3 mt-5">
          <input
            type="text"
            name="country"
            className="form-control"
            id="floatingInput"
            placeholder="name@example.com"
          />
          <label htmlFor="floatingInput">Country..</label>
        </div>

        <div className="form-floating mb-3">
          <input
            type="text"
            name="capital"
            className="form-control"
            id="floatingPassword"
            placeholder="Password"
          />
          <label htmlFor="floatingPassword">Capital...</label>
        </div>

        <button className="btn btn-primary w-100 py-2" type="submit">
          Submit
        </button>
      </form>
    </main>
  );
}

export default CapitalQuiz;
