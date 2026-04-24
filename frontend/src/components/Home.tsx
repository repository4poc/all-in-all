import Card from "./Home/Card";
import Carousel from "./Home/Carousel";
import Feature from "./Home/Feature";
import Hero from "./Home/Hero";
import Mozac from "./Home/Mozaic";
import Poster from "./Home/Poster";
import PricingBox from "./Home/PricingBox";

function Home() {
  return (
    <>
      <Hero />
      <Feature />
      <Carousel />
      <Mozac />
      <Poster />
      <PricingBox />
      <Card />
    </>
  );
}

export default Home;
