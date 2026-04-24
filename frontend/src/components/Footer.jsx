function Footer() {
  return (
    <div id="flex-container my-5">
      <div className="container-fluid text-white">
        <footer className="py-3 my-4 ">
          <ul className="nav justify-content-center border-bottom pb-3 mb-3">
            <li className="nav-item">
              <a href="#" className="nav-link px-2">
                Home
              </a>
            </li>
            <li className="nav-item">
              <a href="#" className="nav-link px-2 ">
                Features
              </a>
            </li>
            <li className="nav-item">
              <a href="#" className="nav-link px-2">
                Pricing
              </a>
            </li>
            <li className="nav-item">
              <a href="#" className="nav-link px-2">
                FAQs
              </a>
            </li>
            <li className="nav-item">
              <a href="#" className="nav-link px-2">
                About
              </a>
            </li>
          </ul>
          <p className="text-center">© 2026 Company, Inc</p>
        </footer>
      </div>
    </div>
  );
}

export default Footer;
