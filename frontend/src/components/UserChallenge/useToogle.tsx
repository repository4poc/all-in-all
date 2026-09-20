import { useState } from "react";

const useToggle = (initState: boolean) => {
  const [flag, setFlag] = useState(initState);

  const handleFlag = () => {
    setFlag(!flag);
  };

  return { flag, handleFlag };
};

export default useToggle;
