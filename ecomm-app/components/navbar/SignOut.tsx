'use client';

import { SignOutButton } from '@clerk/nextjs';
import { toast } from '../ui/toast';

export default function SignOut() {
  const onClickHandler = () => {
    toast.add({
      type: 'success',
      description: 'Logout Successful.',
    });
  };

  return (
    <SignOutButton>
      <button
        type='button'
        className='capitalize w-full text-left mx-1'
        onClick={onClickHandler}
      >
        Logout
      </button>
    </SignOutButton>
  );
}
