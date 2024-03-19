#!/bin/bash

if [ "$#" -ne 1 ]; then
  echo "Usage: $0 TAG"
  exit 1
fi

TAG="$1"
IMAGE_NAME="service-reapprovisionnement"

docker build -t $IMAGE_NAME:$TAG .
docker tag $IMAGE_NAME:$TAG ghcr.io/laurich-app/$IMAGE_NAME:$TAG