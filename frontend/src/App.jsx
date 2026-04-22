function App() {
  return (
    <>
      <div id="navbar">
        <p>Page Layout Methods</p>
        <ul>
          <li>
            <a href="#">HTML Table</a>
          </li>
          <li>
            {" "}
            <a href="#">Inner Block</a>
          </li>
          <li>
            <a href="#">Absolute Positioning</a>
          </li>
          <li>
            <a href="#">Float</a>
          </li>
          <li>
            <a href="#">Flexbox</a>
          </li>
        </ul>
      </div>
      <div class="poster">
        <img src="/assets/img1.jpg" alt="My image"></img>
        <h1>Title</h1>
        <p>Description of the Image</p>
        <div id="blue-box">
          <div id="red-circle"></div>
        </div>
      </div>
    </>
  );
}

export default App;
