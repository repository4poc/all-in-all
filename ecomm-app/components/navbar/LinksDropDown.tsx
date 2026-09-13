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
import SignOut from './SignOut';
import { toast } from '../ui/toast';

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
                  <button className='capitalize w-full text-left mx-1'>
                    Login
                  </button>
                </SignInButton>
              </DropdownMenuItem>

              <DropdownMenuSeparator />

              <DropdownMenuItem>
                <SignUpButton mode='modal'>
                  <button className='capitalize w-full text-left mx-1'>
                    Register
                  </button>
                </SignUpButton>
              </DropdownMenuItem>
            </>
          ) : (
            <>
              {links.map((link) => (
                <DropdownMenuItem key={link.href}>
                  <Link
                    href={link.href}
                    className='capitalize w-full text-left mx-1'
                  >
                    {link.label}
                  </Link>
                </DropdownMenuItem>
              ))}

              <DropdownMenuSeparator />

              <DropdownMenuItem>
                <SignOut />
              </DropdownMenuItem>
            </>
          )}
        </DropdownMenuGroup>
      </DropdownMenuContent>
    </DropdownMenu>
  );
}
