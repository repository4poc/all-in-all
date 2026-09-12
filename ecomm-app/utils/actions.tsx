'use server';

import { readFile, writeFile } from 'fs/promises';
import { revalidatePath } from 'next/cache';

type User = {
  id: string;
  firstName: string;
  lastName: string;
};

export async function createUser(formData: FormData) {
  const firstName = formData.get('firstName') as string;
  const lastName = formData.get('lastName') as string;

  const newUser: User = {
    id: Date.now().toString(),
    firstName,
    lastName,
  };

  await saveUser(newUser);

  revalidatePath('/action');
}

export const fetchUsers = async (): Promise<User[]> => {
  const result = await readFile('users.json', { encoding: 'utf8' });
  return result ? JSON.parse(result) : [];
};

export const saveUser = async (user: User): Promise<void> => {
  const users = await fetchUsers();

  users.push(user);

  await writeFile('users.json', JSON.stringify(users, null, 2), {
    encoding: 'utf8',
  });
};
