#!/bin/bash
set -e

aws ecr get-login-password --region us-east-2 --profile weather-ecr-agent | docker login --username AWS --password-stdin 743297763361.dkr.ecr.us-east-2.amazonaws.com docker build -f ./Dockerfile -t cloud-weather-temperature:latest . docker tag cloud-weather-temperature:latest 743297763361.dkr.ecr.us-east-2.amazonaws.com/cloud-weather-temperature:latest docker push 743297763361.dkr.ecr.us-east-2.amazonaws.com/cloud-weather-temperature:latest