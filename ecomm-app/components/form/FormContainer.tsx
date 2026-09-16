'use client';

import { useFormState } from 'react-dom';
import { useActionState, useEffect } from 'react';
import { toast } from '../ui/toast';

import { actionFunction } from '@/utils/types';

const initialState = {
  message: '',
};

function FormContainer({
  action,
  children,
}: {
  action: actionFunction;
  children: React.ReactNode;
}) {
  const [state, formAction] = useActionState(action, initialState);
  useEffect(() => {
    if (state.message) {
      toast.add({
        type: 'success',
        description: state.message,
      });
    }
  }, [state]);
  return <form action={formAction}>{children}</form>;
}
export default FormContainer;
