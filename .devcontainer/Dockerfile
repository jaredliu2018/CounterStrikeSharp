FROM registry.gitlab.steamos.cloud/steamrt/sniper/sdk:latest

# Bullseye security packages have moved off the live security mirror.
# Fix the base image's sources before Dev Container features install packages.
RUN sed -i 's|http://security.debian.org/debian-security|https://archive.debian.org/debian-security|g' /etc/apt/sources.list \
    && rm -rf /var/lib/apt/lists/*
