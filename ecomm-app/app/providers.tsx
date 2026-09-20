'use client';

import { Toaster } from '@/components/ui/toast';
import ThemeProvider from './theme-provider';
import { ApolloProvider } from '@apollo/client/react';
import client from '@/utils/demos/github/apolloClient';

export default function Providers({ children }: { children: React.ReactNode }) {
  return (
    <>
      <ApolloProvider client={client}>
        <Toaster />
        <ThemeProvider
          attribute='class'
          defaultTheme='system'
          enableSystem
          disableTransitionOnChange
        >
          {children}
        </ThemeProvider>
      </ApolloProvider>
    </>
  );
}
