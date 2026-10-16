resource "aws_vpc" "vpc_north" {
  provider   = aws
  cidr_block = "10.0.0.0/16"

  tags = {
    Name = "vpc-north"
  }
}

resource "aws_vpc" "vpc_west" {
  provider   = aws.west
  cidr_block = "10.1.0.0/16"

  tags = {
    Name = "vpc-west"
  }
}

resource "aws_subnet" "subnet" {
  vpc_id     = aws_vpc.vpc_north.id
  cidr_block = "10.0.1.0/24"

  tags = {
    Name = "subnet-north"
  }
}


resource "aws_instance" "aws_ec2" {
  ami           = "resolve:ssm:/aws/service/ami-amazon-linux-latest/al2023-ami-kernel-default-x86_64"
  instance_type = "t3.micro"
  subnet_id     = aws_subnet.subnet.id

  cpu_options {
    threads_per_core = 2
  }

  tags = {
    Name = "aws_ec2"
  }
}
