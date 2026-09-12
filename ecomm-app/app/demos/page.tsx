import DemosList from '@/components/demos/DemosList';
import { demos } from '@/utils/demos';

export default function Demos() {
  return (
    <>
      <DemosList demos={demos} />
    </>
  );
}
