import Image from 'next/image';
import LandingZone from '@/public/images/CustomLandingZone.png';

export default function page() {
  return (
    <div>
      <Image src={LandingZone} alt='Landing Zone'></Image>
    </div>
  );
}
