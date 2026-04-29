function Feature() {
  return (
    <div className="text-white">
      <div className="container px-4 py-5" id="featured-3">
        <h2 className="pb-2 border-bottom ">Columns with icons</h2>

        <div className="row g-4 py-5 row-cols-1 row-cols-lg-3">
          {/* Feature 1 */}
          <div className="feature col">
            <div
              className="feature-icon d-inline-flex align-items-center justify-content-center text-bg-primary 
              bg-gradient fs-2 mb-3"
            >
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="30"
                height="30"
                fill="currentColor"
                className="bi bi-apple-music"
                viewBox="0 0 16 16"
              >
                <path
                  fill-rule="evenodd"
                  d="m10.995 0 .573.001q.241 0 .483.007c.35.01.705.03 1.051.093.352.063.68.166.999.329a3.36 3.36 0 0 1 1.47 1.468c.162.32.265.648.328 1 .063.347.084.7.093 1.051q.007.241.007.483l.001.573v5.99l-.001.573q0 .241-.008.483c-.01.35-.03.704-.092 1.05a3.5 3.5 0 0 1-.33 1 3.36 3.36 0 0 1-1.468 1.468 3.5 3.5 0 0 1-1 .33 7 7 0 0 1-1.05.092q-.241.007-.483.008l-.573.001h-5.99l-.573-.001q-.241 0-.483-.008a7 7 0 0 1-1.052-.092 3.6 3.6 0 0 1-.998-.33 3.36 3.36 0 0 1-1.47-1.468 3.6 3.6 0 0 1-.328-1 7 7 0 0 1-.093-1.05Q.002 11.81 0 11.568V5.005l.001-.573q0-.241.007-.483c.01-.35.03-.704.093-1.05a3.6 3.6 0 0 1 .329-1A3.36 3.36 0 0 1 1.9.431 3.5 3.5 0 0 1 2.896.1 7 7 0 0 1 3.95.008Q4.19.002 4.432 0h.573zm-.107 2.518-4.756.959H6.13a.66.66 0 0 0-.296.133.5.5 0 0 0-.16.31c-.004.027-.01.08-.01.16v5.952c0 .14-.012.275-.106.39-.095.115-.21.15-.347.177l-.31.063c-.393.08-.65.133-.881.223a1.4 1.4 0 0 0-.519.333 1.25 1.25 0 0 0-.332.995c.031.297.166.582.395.792.156.142.35.25.578.296.236.047.49.031.858-.043.196-.04.38-.102.555-.205a1.4 1.4 0 0 0 .438-.405 1.5 1.5 0 0 0 .233-.55c.042-.202.052-.386.052-.588V6.347c0-.276.08-.35.302-.404.024-.005 3.954-.797 4.138-.833.257-.049.378.025.378.294v3.524c0 .14-.001.28-.096.396-.094.115-.211.15-.348.178l-.31.062c-.393.08-.649.133-.88.223a1.4 1.4 0 0 0-.52.334 1.26 1.26 0 0 0-.34.994c.03.297.174.582.404.792a1.2 1.2 0 0 0 .577.294c.237.048.49.03.858-.044.197-.04.381-.098.556-.202a1.4 1.4 0 0 0 .438-.405q.173-.252.233-.549a2.7 2.7 0 0 0 .044-.589V2.865c0-.273-.143-.443-.4-.42-.04.003-.383.064-.424.073"
                />
              </svg>
            </div>

            <h3 className="fs-2 text-white">Featured title</h3>

            <p>
              Paragraph of text beneath the heading to explain the heading.
              We'll add onto it with another sentence and probably just keep
              going until we run out of words.
            </p>

            <a href="#" className="icon-link">
              Call to action
              <svg className="bi ms-2" aria-hidden="true">
                <use xlinkHref="#chevron-right"></use>
              </svg>
            </a>
          </div>

          {/* Feature 2 */}
          <div className="feature col">
            <div className="feature-icon d-inline-flex align-items-center justify-content-center text-bg-primary bg-gradient fs-2 mb-3">
              <svg
                xmlns="http://www.w3.org/2000/svg"
                width="30"
                height="30"
                fill="currentColor"
                className="bi bi-briefcase-fill"
                viewBox="0 0 16 16"
              >
                <path d="M6.5 1A1.5 1.5 0 0 0 5 2.5V3H1.5A1.5 1.5 0 0 0 0 4.5v1.384l7.614 2.03a1.5 1.5 0 0 0 .772 0L16 5.884V4.5A1.5 1.5 0 0 0 14.5 3H11v-.5A1.5 1.5 0 0 0 9.5 1zm0 1h3a.5.5 0 0 1 .5.5V3H6v-.5a.5.5 0 0 1 .5-.5" />
                <path d="M0 12.5A1.5 1.5 0 0 0 1.5 14h13a1.5 1.5 0 0 0 1.5-1.5V6.85L8.129 8.947a.5.5 0 0 1-.258 0L0 6.85z" />
              </svg>
            </div>

            <h3 className="fs-2 text-white">Featured title</h3>

            <p>
              Paragraph of text beneath the heading to explain the heading.
              We'll add onto it with another sentence and probably just keep
              going until we run out of words.
            </p>

            <a href="#" className="icon-link">
              Call to action
              <svg className="bi ms-2" aria-hidden="true">
                <use xlinkHref="#chevron-right"></use>
              </svg>
            </a>
          </div>

          {/* Feature 3 */}
          <div className="feature col">
            <div className="feature-icon d-inline-flex align-items-center justify-content-center text-bg-primary bg-gradient fs-2 mb-3">
              <img
                src="\public\assets\telephone-fill.svg"
                style={{ height: "30px", width: "30px" }}
              ></img>
            </div>

            <h3 className="fs-2 text-white">Featured title</h3>

            <p>
              Paragraph of text beneath the heading to explain the heading.
              We'll add onto it with another sentence and probably just keep
              going until we run out of words.
            </p>

            <a href="#" className="icon-link">
              Call to action
              <svg className="bi ms-2" aria-hidden="true">
                <use xlinkHref="#chevron-right"></use>
              </svg>
            </a>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Feature;
