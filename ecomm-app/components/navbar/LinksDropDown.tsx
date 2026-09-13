'use client';

import {
  BadgeCheckIcon,
  BellIcon,
  CreditCardIcon,
  LogOutIcon,
} from 'lucide-react';

import { Avatar, AvatarFallback, AvatarImage } from '@/components/ui/avatar';
import { Button } from '@/components/ui/button';
import {
  DropdownMenu,
  DropdownMenuContent,
  DropdownMenuGroup,
  DropdownMenuItem,
  DropdownMenuSeparator,
  DropdownMenuTrigger,
} from '@/components/ui/dropdown-menu';

import { links } from '@/utils/links';
import Link from 'next/link';
import { useAuth, SignInButton, SignUpButton } from '@clerk/nextjs';
import SignOutLink from './SignOutLink';

export function LinksDropDown() {
  const { userId } = useAuth();
  const isSignedIn = !!userId;

  return (
    <DropdownMenu>
      <DropdownMenuTrigger
        render={
          <Button variant='ghost' size='icon' className='rounded-full'>
            <Avatar>
              <AvatarImage src='/Varinder_Photo.jpeg' alt='avatar' />
              <AvatarFallback>LR</AvatarFallback>
            </Avatar>
          </Button>
        }
      />

      <DropdownMenuContent align='end' className='w-30' sideOffset={5}>
        <DropdownMenuGroup>
          {!isSignedIn ? (
            <>
              <DropdownMenuItem>
                <SignInButton mode='modal'>
                  <button>Login</button>
                </SignInButton>
              </DropdownMenuItem>

              <DropdownMenuSeparator />

              <DropdownMenuItem>
                <SignUpButton mode='modal'>
                  <button>Register</button>
                </SignUpButton>
              </DropdownMenuItem>
            </>
          ) : (
            <>
              {links.map((link) => (
                <DropdownMenuItem key={link.href}>
                  <Link href={link.href} className='capitalize'>
                    {link.label}
                  </Link>
                </DropdownMenuItem>
              ))}

              <DropdownMenuSeparator />

              <DropdownMenuItem>
                <SignOutLink />
              </DropdownMenuItem>
            </>
          )}
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
