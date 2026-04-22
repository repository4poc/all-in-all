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

      <div class="pricing-container">
        <div class="pricing-plan">
          <div class="plan-title">Basic</div>
          <div class="plan-price">$9.99/month</div>
          <ul class="plan-features">
            <li>✅ 10GB Storage</li>
            <li>✅ 1 User</li>
            <li>🚫 Support</li>
          </ul>
          <button class="plan-button">Sign Up</button>
        </div>
        <div class="pricing-plan">
          <div class="plan-title">Standard</div>
          <div class="plan-price">$19.99/month</div>
          <ul class="plan-features">
            <li>✅ 50GB Storage</li>
            <li>✅ 5 Users</li>
            <li>✅ Phone &amp; Email Support</li>
          </ul>
          <button class="plan-button">Sign Up</button>
        </div>
        <div class="pricing-plan">
          <div class="plan-title">Premium</div>
          <div class="plan-price">$49.99/month</div>
          <ul class="plan-features">
            <li>✅ 100GB Storage</li>
            <li>✅ 10 Users</li>
            <li>✅ 24/7 Support</li>
          </ul>
          <button class="plan-button">Sign Up</button>
        </div>
      </div>
    </>
  );
}

export default App;
