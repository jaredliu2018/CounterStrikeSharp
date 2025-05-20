## The update steps are as following:
1. git fetch upstream
2. git checkout main
3. git merge upstream/main
4. git checkout dev_clientmessage
5. git merge main (solve conflicts if necessary)
6. git submodule update --init --recursive (if necessary)
7. reopen in container
8. rm -rf build (if necessary)
9. mkdir -p build && cd build
10. cmake -G Ninja -DCMAKE_BUILD_TYPE=Release ..
11. cmake --build . --config Release -- -j16
12. upload API to nuget (if necessary) and update the API version in all plugins project file (.csproj)

## After merge dev_clientmessage from main, remember to change devcontainer.json back as following:
```
{
  "name": "SteamRT Sniper SDK",
  "image": "registry.gitlab.steamos.cloud/steamrt/sniper/sdk:latest",
  "customizations": {
    "vscode": {
      "extensions": [
        "ms-vscode.cpptools"
      ]
    }
  }
}
```
Otherwise, docker build will fail because of network issues.

## When you want to update the submodules, run following command:
```
git config --global http.proxy "socks5://127.0.0.1:10808"
git config --global https.proxy  "socks5://127.0.0.1:10808"
git submodule update --init --recursive
```
Don't forget to unset the proxy after you finish, because docker will import git proxy settings from your host.
```
git config --global --unset http.proxy
git config --global --unset https.proxy
```
