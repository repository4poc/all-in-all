function PricingBox() {
  return (
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
  );
}

export default PricingBox;
