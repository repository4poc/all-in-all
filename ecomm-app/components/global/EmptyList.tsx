import React from 'react';

export default function EmptyList({
  text = 'No Items Found.',
}: {
  text: String;
}) {
  return (
    <>
      <h2>{text}</h2>
    </>
  );
}
