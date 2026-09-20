import { GET_USER } from '@/utils/demos/github/queries';
import { UserData } from '@/utils/demos/github/types';
import { useQuery } from '@apollo/client/react';
import React from 'react';
import UserCard from './UserCard';
import StatsContainer from './StatsContainer';
import UsedLanguages from './UsedLanguages';
import PopularRepos from './PopularRepos';
import ForkedRepos from './ForkedRepos';

type UserProfileProps = {
  userName: string;
};

export default function UserProfile({ userName }: UserProfileProps) {
  const { data, loading, error } = useQuery<UserData>(GET_USER, {
    variables: { login: userName },
  });

  if (error) return <h2 className='text-xl'>{error.message}</h2>;
  if (!data) return <h2 className='text-xl'>User Not Found.</h2>;

  const {
    avatarUrl,
    name,
    bio,
    url,
    repositories,
    followers,
    following,
    gists,
  } = data.user;

  return (
    <>
      <UserCard avatarUrl={avatarUrl} name={name} bio={bio} url={url} />

      <StatsContainer
        totalRepos={repositories.totalCount}
        followers={followers.totalCount}
        following={following.totalCount}
        gists={gists.totalCount}
      />
      {repositories.totalCount > 0 && (
        <div className='grid md:grid-cols-2 gap-4'>
          <UsedLanguages repositories={repositories.nodes} />
          <PopularRepos repositories={repositories.nodes} />
        </div>
      )}
    </>
  );
}
