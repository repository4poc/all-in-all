import { PageLayout } from "./components/PageLayout";

import {
  AuthenticatedTemplate,
  UnauthenticatedTemplate,
} from "@azure/msal-react";
import "./App.css";
import Footer from "./components/Footer";
import { Route, Routes } from "react-router-dom";
import QRGenerator from "./components/QRGenerator";
import Dice from "./components/Dice";
import Home from "./components/Home";
import Drum from "./components/Drum";
import CapitalQuiz from "./components/CapitalQuiz";
import ChatWindow from "./components/ChatWindow";

/**
 * If a user is authenticated the ProfileContent component above is rendered. Otherwise a message indicating a user is not authenticated is rendered.
 */
const MainContent = () => {
  return (
    <div className="App">
      <AuthenticatedTemplate>
        <>
          <main>
            <Routes>
              <Route path="/" element={<Home />} />
              <Route path="/dice" element={<Dice />} />
              <Route path="/drum" element={<Drum />} />
              <Route path="/qrcode" element={<QRGenerator />} />
              <Route path="/capitalquiz" element={<CapitalQuiz />} />
              <Route path="/chatwindow" element={<ChatWindow />} />
            </Routes>
          </main>

          <br></br>
          <br></br>
          <br></br>
          <Footer />
        </>
      </AuthenticatedTemplate>

      <UnauthenticatedTemplate>
        <h5 className="card-title">
          Please sign-in to see your profile information.
        </h5>
      </UnauthenticatedTemplate>
    </div>
  );
};

export default function App() {
  return (
    <PageLayout>
      <MainContent />
    </PageLayout>
  );
}
