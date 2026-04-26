function NavBar() {
  return (
    <div>
      <nav
        className="navbar bg-dark navbar-expand-lg bg-body-tertiary"
        data-bs-theme="dark"
      >
        <div className="container-fluid">
          <a className="navbar-brand" href="#">
            <img
              src="/public/assets/vg_logo.svg"
              alt="Logo"
              width="40"
              height="40"
              className="d-inline-block align-text-top"
            />
          </a>

          <button
            className="navbar-toggler"
            type="button"
            data-bs-toggle="collapse"
            data-bs-target="#navbarNavDropdown"
            aria-controls="navbarNavDropdown"
            aria-expanded="false"
            aria-label="Toggle navigation"
          >
            <span className="navbar-toggler-icon"></span>
          </button>

          <div className="collapse navbar-collapse" id="navbarNavDropdown">
            <ul className="navbar-nav">
              <li className="nav-item">
                <a className="nav-link active" aria-current="page" href="/">
                  Home
                </a>
              </li>

              <li className="nav-item">
                <a className="nav-link" href="#">
                  Features
                </a>
              </li>

              <li className="nav-item">
                <a className="nav-link" href="#">
                  Pricing
                </a>
              </li>

              <li className="nav-item dropdown">
                <a
                  className="nav-link dropdown-toggle"
                  href="#"
                  role="button"
                  data-bs-toggle="dropdown"
                  aria-expanded="false"
                >
                  Games
                </a>

                <ul className="dropdown-menu">
                  <li>
                    <a className="dropdown-item" href="/dice">
                      Dice Game
                    </a>
                  </li>
                  <li>
                    <a className="dropdown-item" href="/drum">
                      Drump Game
                    </a>
                  </li>
                  <li>
                    <a className="dropdown-item" href="/qrcode">
                      QR Code Generator
                    </a>
                  </li>
                </ul>
              </li>
            </ul>
          </div>
        </div>
      </nav>
    </div>
  );
}

export default NavBar;
