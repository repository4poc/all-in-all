import { useIsAuthenticated } from "@azure/msal-react";
import { SignInButton } from "./SignInButton";
import { SignOutButton } from "./SignOutButton";
import { useMsal } from "@azure/msal-react";
import Button from "react-bootstrap/Button";
import { useState } from "react";
import { loginRequest } from "../authConfig";
import { callMsGraph } from "../graph";
import { ProfileData } from "./ProfileData";

/**
 * Renders the navbar component with a sign-in or sign-out button depending on whether or not a user is authenticated
 * @param props
 */
interface PageLayoutProps {
  children: React.ReactNode;
}

const ProfileContent = () => {
  const { instance, accounts } = useMsal();

  const [graphData, setGraphData] = useState(null);

  function RequestProfileData() {
    // Silently acquires an access token which is then attached to a request for MS Graph data
    instance
      .acquireTokenSilent({
        ...loginRequest,
        account: accounts[0],
      })
      .then((response) => {
        callMsGraph(response.accessToken).then((response) =>
          setGraphData(response),
        );
      });
  }

  return (
    <>
      {graphData ? (
        <ProfileData graphData={graphData} />
      ) : (
        <Button variant="secondary" onClick={RequestProfileData}>
          {accounts[0].name
            ?.split(" ")[0]
            ?.replace(/^./, (c) => c.toUpperCase())}
        </Button>
      )}
    </>
  );
};

export const PageLayout = (props: PageLayoutProps) => {
  const isAuthenticated = useIsAuthenticated();

  return (
    <>
      <nav
        className="navbar bg-dark navbar-expand-lg bg-body-tertiary"
        data-bs-theme="dark"
      >
        <div className="container-fluid">
          <a className="navbar-brand" href="#">
            <img
              src="/assets/vg_logo.svg"
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
          {isAuthenticated ? (
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
                    <li>
                      <a className="dropdown-item" href="/capitalquiz">
                        Capital Quiz Game
                      </a>
                    </li>
                    <li>
                      <a className="dropdown-item" href="/bookshop">
                        Book Shop
                      </a>
                    </li>
                    <li>
                      <a className="dropdown-item" href="/chatwindow">
                        AI Chat
                      </a>
                    </li>
                  </ul>
                </li>
              </ul>
            </div>
          ) : (
            <div />
          )}
        </div>
        <div className="collapse navbar-collapse justify-content-end">
          {isAuthenticated ? (
            <>
              &nbsp;
              <ProfileContent /> &nbsp;
              <SignOutButton />
            </>
          ) : (
            <SignInButton />
          )}
        </div>
      </nav>
      <div className="profileContent">{props.children}</div>
    </>
  );
};
