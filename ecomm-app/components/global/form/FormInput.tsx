import React from 'react';
import { Label } from '../../ui/label';
import { Input } from '../../ui/input';

type FormInputProps = {
  name: string;
  type: string;
  label?: string;
  defaultValue?: string;
  placeholder?: string;
};

export default function FormInput({
  name,
  type,
  label,
  defaultValue,
  placeholder,
}: FormInputProps) {
  return (
    <div className='md-2'>
      <Label htmlFor={name} className='capitalize'>
        {label}
      </Label>
      <Input
        id={name}
        name={name}
        type={type}
        className='mt-2'
        defaultValue={defaultValue}
        required
      />
    </div>
  );
}
