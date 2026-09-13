'use client';

import { SignOutButton } from '@clerk/nextjs';
import { toast } from '../ui/toast';

export default function SignOut() {
  const onClickHandler = () => {
    console.log('------');

    toast.add({
      type: 'success',
      description: 'Logout Successful.',
    });
  };

  return (
    <SignOutButton>
      <button type='button' onClick={onClickHandler}>
        Logout
      </button>
    </SignOutButton>
  );
}
