import { Button } from '@/components/ui/button';
import { Input } from '@/components/ui/input';
import { Label } from '@/components/ui/label';
import React, { FormEvent, useState } from 'react';
import { toast } from '@/components/ui/toast';

type SearchFormProps = {
  userName: string;
  setUserName: React.Dispatch<React.SetStateAction<string>>;
};

export default function SearchForm({ userName, setUserName }: SearchFormProps) {
  const [name, setName] = useState(userName);

  const handleSearch = (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    if (name === '') {
      toast.add({
        type: 'success',
        description: 'Please enter a name',
      });
      return;
    }
    setName(name);
  };
  return (
    <>
      <form onSubmit={handleSearch} className='flex'>
        <Input
          type='text'
          id='search'
          value={name}
          onChange={(e) => setName(e.target.value)}
          placeholder='Search GitHub User'
          className='bg-background'
        ></Input>
        <Button type='submit'>Search</Button>
      </form>
    </>
  );
}
