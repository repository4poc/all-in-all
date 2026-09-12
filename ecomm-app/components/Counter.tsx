'use client';

import { useState } from 'react';

function Counter() {
  const [count, setCount] = useState(0);

  const handleCount = () => {
    setCount(count + 1);
  };

  return (
    <div className='flex flex-col items-center'>
      <p className='text-3xl text-blue-700 font-bold'>{count}</p>
      <button className='bg-blue-700 rounded p-1' onClick={handleCount}>
        Count
      </button>
    </div>
  );
}

export default Counter;
