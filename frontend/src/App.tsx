import NavBar from "./components/NavBar";
import Footer from "./components/Footer";
import { Route, Routes } from "react-router-dom";
import Home from "./components/Home";
import Dice from "./components/Dice";
import Drum from "./components/Drum";

function App() {
  return (
    <>
      <NavBar />

      <main>
        <Routes>
          <Route path="/" element={<Home />} />
          <Route path="/dice" element={<Dice />} />
          <Route path="/drum" element={<Drum />} />
          <Route path="/contact" element={<Home />} />
        </Routes>
      </main>

      <br></br>
      <br></br>
      <br></br>
      <Footer />
    </>
  );
}

export default App;
